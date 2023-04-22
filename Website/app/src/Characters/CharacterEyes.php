<?php

namespace App\CharacterDatabase;

use SilverStripe\Assets\Image;
use SilverStripe\ORM\DataObject;
use SilverStripe\Security\Member;
use SilverStripe\Security\Permission;

/**
 * Class \App\Database\Experience
 *
 * @property string $VisitTime
 * @property string $Weather
 * @property string $Notes
 * @property string $Score
 * @property int $Podest
 * @property string $Train
 * @property int $Wagon
 * @property int $Row
 * @property int $Seat
 * @property string $Variant
 * @property string $Version
 * @property int $UserID
 * @property int $ExperienceID
 * @method \SilverStripe\Security\Member User()
 * @method \App\ExperienceDatabase\Experience Experience()
 * @method \SilverStripe\ORM\ManyManyList|\SilverStripe\Security\Member[] Friends()
 */
class CharacterEyes extends DataObject
{
    private static $db = [
        "Title" => "Varchar(255)",
        "RequiredXP" => "Int",
    ];

    private static $has_one = [
        "ForegroundImage" => Image::class,
        "BackgroundImage" => Image::class,
    ];

    private static $belongs_many = [
        "Members" => Member::class,
    ];

    private static $summary_fields = [
        "Title" => "Title",
        "RequiredXP" => "Required XP",
    ];

    private static $field_labels = [
    ];

    private static $default_sort = "RequiredXP ASC";

    private static $table_name = "CharacterHat";

    private static $singular_name = "Eyes";
    private static $plural_name = "Eyes";

    private static $url_segment = "character-eyes";

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();
        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
